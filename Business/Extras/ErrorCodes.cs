namespace Business.Extras
{
    public static class ErrorCodes
    {
        public const string UserNotFound = "E001";
        public const string InvalidCredentials = "E002";
        public const string DatabaseError = "E003";
        public const string ValidationError = "E004";

        public static string GetMessage(string code) => code switch
        {
            UserNotFound => "Usuário não encontrado.",
            InvalidCredentials => "Credenciais inválidas.",
            DatabaseError => "Erro ao acessar o banco de dados.",
            ValidationError => "Dados fornecidos são inválidos.",
            _ => "Erro desconhecido."
        };
    }
}
