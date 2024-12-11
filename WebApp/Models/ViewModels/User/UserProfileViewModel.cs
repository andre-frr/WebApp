namespace WebApp.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string nome { get; set; }
        public string apelido { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string morada { get; set; }
        public int nif { get; set; }
        public string cidade { get; set; }
        public string? cod_postal { get; set; }

        //Propriedades para o update da password
        public string newPass { get; set; }
        public string confirmPass { get; set; }
    }
}
