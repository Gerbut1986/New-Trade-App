namespace New_Trade_Soft_App.Models
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Collections.ObjectModel;

    public class ProjectModel : BaseModel
    {
        public ProjectModel()
        {
            _Inputs = new ObservableCollection<MT5Model>();
            _ConnectInfo = new ObservableCollection<ConnectionModel>();
        }

        ObservableCollection<ConnectionModel> _ConnectInfo;
        public ObservableCollection<ConnectionModel> ConnectInfo
        {
            get { return _ConnectInfo; }
            set { if (_ConnectInfo != value) { _ConnectInfo = value; OnPropertyChanged(); } }
        }

        ObservableCollection<MT5Model> _Inputs;
        public ObservableCollection<MT5Model> Inputs
        {
            get { return _Inputs; }
            set { if (_Inputs != value) { _Inputs = value; OnPropertyChanged(); } }
        }

        static XmlSerializer CreateSerializer()
        {
            Type[] types_to_serialize = new Type[] { typeof(BaseModel), typeof(MT5Model) };
            return new XmlSerializer(typeof(ProjectModel), types_to_serialize);
        }

        public static ProjectModel Load(string filename)
        {
            ProjectModel res = null;
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    var xs = CreateSerializer();
                    res = xs.Deserialize(fs) as ProjectModel;
                }
            }
            catch { res = null; }
            if (res == null) res = new ProjectModel();

            if (res.Inputs == null) res.Inputs = new ObservableCollection<MT5Model>();

            return res;
        }

        public void Save(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    var xs = CreateSerializer();
                    xs.Serialize(fs, this);
                }
            }
            catch { }
        }
    }
}
