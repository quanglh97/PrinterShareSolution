using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class AppVersionFile
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
        public string FilePathSetup { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string Md5 { get; set; }
        public DateTime DateTime { get; set; }
    }
}
