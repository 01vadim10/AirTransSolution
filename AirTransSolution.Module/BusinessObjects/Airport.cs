using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace AirTransSolution.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Airport : BaseObject
    { 
        public Airport(Session session)
            : base(session)
        {
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("name", ref name, value); }
        }

//        private IList<Pilot> pilots;
//        public Pil
    }

    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Name")]
    public class Pilot : BaseObject
    {
        public Pilot(Session session) : base(session) { }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        private IList<Airplane> airplanes;

        public IList<Airplane> Airplanes
        {
            get { return airplanes; }
            set { SetPropertyValue("Airplanes", ref airplanes, value); }
        }
    }

    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Name")]
    public class Airplane : BaseObject
    {
        public Airplane(Session session) : base(session) { }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
    }
}