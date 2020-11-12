namespace New_Trade_Soft_App.Connectors
{
    using New_Trade_Soft_App.Models;
    using System.Collections.Generic;

    internal class ConnectorRefs
    {
        public IConnector Connector { get; set; }
        public int Refs { get; set; }
    }

    internal class ConnectorsFactory
    {
        public static ConnectorsFactory Current = new ConnectorsFactory();

        Dictionary<string, ConnectorRefs> connectors;

        public ConnectorsFactory()
        {
            connectors = new Dictionary<string, ConnectorRefs>();
        }

        //public IConnector CreateMetatrader4(IConnectorLogger logger, ConnectionModel model)
        //{
        //    lock (connectors)
        //    {
        //        var connector = CreateExist(model.Name);
        //        if (connector != null) return connector;
        //        Metatrader4Client client = new Metatrader4Client(logger,model.Login, model.Password, Helpers.SrvFolder.Path(model.Srv));
        //        return CreateCRef(client, model.Name);
        //    }
        //}

        public IConnector CreateMetatrader5(IConnectorLogger logger, ConnectionModel model)
        {
            lock (connectors)
            {
                var connector = CreateExist(model.Name);
                if (connector != null) return connector;
                Metatrader5Client client = new Metatrader5Client(logger, model.Username, model.Password, model.Address);
                return CreateCRef(client, model.Name);
            }
        }

        IConnector CreateCRef(IConnector client, string connectionName)
        {
            client.Start();
            ConnectorRefs cref = new ConnectorRefs();
            cref.Connector = client;
            cref.Refs = 1;
            connectors[connectionName] = cref;

            return cref.Connector;
        }

        IConnector CreateExist(string connectionName)
        {
            if (connectors.ContainsKey(connectionName))
            {
                var cref = connectors[connectionName];
                cref.Refs++;
                return cref.Connector;
            }

            return null;
        }

        public void CloseConnector(string connectionName)
        {
            lock (connectors)
            {
                if (connectors.ContainsKey(connectionName))
                {
                    var cref = connectors[connectionName];
                    cref.Refs--;
                    if (cref.Refs <= 0)
                    {
                        cref.Connector.Stop();
                        connectors.Remove(connectionName);
                    }
                }
            }
        }

        public void CloseAll()
        {
            lock (connectors)
            {
                foreach (var c in connectors)
                {
                    c.Value.Connector.Stop();
                }
                connectors.Clear();
            }
        }
    }
}
