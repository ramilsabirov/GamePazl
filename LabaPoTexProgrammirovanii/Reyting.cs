using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaPoTexProgrammirovanii
{
    class Reyting
    {
        public int Id { get; set; }
        public int  Time { get; set; }
     

        public string TimeForString()
        {
            
            var ts = TimeSpan.FromSeconds(Time);
            string second ="";
            string minute ="";
            if (ts.Seconds < 10)
                second = "0" + ts.Seconds;
            else
                second = ts.Seconds.ToString() ;
            if (ts.Minutes < 10)
                minute = "0" + ts.Minutes;
            else
                minute = ts.Minutes.ToString();
             
            return string.Format("{0}:{1}", minute, second);
        }   

    }
}
