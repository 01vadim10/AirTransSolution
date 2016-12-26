using System;
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
            set { SetPropertyValue("Name", ref name, value); }
        }

        [Association("Airport-Pilots")]
        public XPCollection<Pilot> Pilots
        {
            get { return GetCollection<Pilot>("Pilots"); }
        }
        
        [Association("Airport-Airplanes")]
        public XPCollection<Airplane> Airplanes
        {
            get { return GetCollection<Airplane>("Airplanes"); }
        }
    }

    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Full Name")]
    public class Pilot : BaseObject
    {
        public Pilot(Session session) : base(session) { }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { SetPropertyValue("FirstName", ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue("LastName", ref lastName, value); }
        }

        private string fullName;

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        [Association("Pilot-Airplanes")]
        public XPCollection<Airplane> Airplanes
        {
            get { return GetCollection<Airplane>("Airplanes"); }
        }

        private Airport airpot;
        [Association("Airport-Pilots")]
        public Airport Airport
        {
            get { return airpot; }
            set { SetPropertyValue("Airport", ref airpot, value); }
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

        [Association("Pilot-Airplanes")]
        public XPCollection<Pilot> Pilots
        {
            get { return GetCollection<Pilot>("Pilots"); }
        }

        private Airport airpot;
        [Association("Airport-Airplanes")]
        public Airport Airport
        {
            get { return airpot; }
            set { SetPropertyValue("Airport", ref airpot, value); }
        }
    }
}