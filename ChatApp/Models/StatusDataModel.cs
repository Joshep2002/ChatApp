using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class StatusDataModel
    {
        public string ContactName {  get; set; }
        public  Uri ContactPhoto {  get; set; }
    //    public string StatusMessage{ get; set; }
        public Uri StatusImage { get; set; }

        // Si queremos agregar nuestro status
        public bool IsMeAddStatus {  get; set; }
    }
}
