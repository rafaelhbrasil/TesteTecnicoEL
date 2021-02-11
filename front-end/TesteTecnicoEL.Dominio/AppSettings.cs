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
