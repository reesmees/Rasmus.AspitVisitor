using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.Business
{
    public class DataHandler
    {
        private DataCreator creator;
        private DataReader reader;
        private DataUpdater updater;
        private AspitVisitorContext db;

        public DataHandler(AspitVisitorContext dB)
        {
            DB = dB;
            Creator = new DataCreator(DB);
            Reader = new DataReader(DB);
            Updater = new DataUpdater(DB);
        }

        public AspitVisitorContext DB
        {
            get { return db; }
            set { db = value; }
        }


        public DataUpdater Updater
        {
            get { return updater; }
            set { updater = value; }
        }

        public DataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        public DataCreator Creator
        {
            get { return creator; }
            set { creator = value; }
        }

    }
}
