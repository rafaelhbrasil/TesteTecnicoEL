using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio
{
    public class AppSettings
    {
        static AppSettings()
        {
            Instance = new AppSettings();
        }
        public static AppSettings Instance { get; private set; }

        public string CaminhoBaseApi { get; set; }
    }
}
