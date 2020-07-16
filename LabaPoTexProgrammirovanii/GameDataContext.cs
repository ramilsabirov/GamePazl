using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LabaPoTexProgrammirovanii
{
    class GameDataContext: DbContext
    {
        public GameDataContext() { }
        public GameDataContext(string connectionString) : base(connectionString) { }
        public DbSet<SaveData> SaveDatas { get; set; }
        public DbSet<Reyting> Reytings{ get; set; }
        
    }
}
