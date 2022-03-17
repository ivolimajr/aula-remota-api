namespace AulaRemota.Shared.Helpers.Constants
{
    public static class Constants
    {
        //As roles também são utilizadas para nomear o container no azure blobs.
        //Ao alterar o nome de uma role, alterar também o nome do container no azure (lá no azure mesmo).
        public static class Roles
        {
            public const string APIUSER = nameof(APIUSER);
            public const string EDRIVING = nameof(EDRIVING);
            public const string AUTOESCOLA = nameof(AUTOESCOLA);
            public const string PARCEIRO = nameof(PARCEIRO);
            public const string ALUNO = nameof(ALUNO);
            public const string INSTRUTOR = nameof(INSTRUTOR);
            public const string ADMINISTRATIVO = nameof(ADMINISTRATIVO);
        }
        public static class EdrivingCargos
        {
            public const string DIRETOR = nameof(DIRETOR);
            public const string ANALISTA = nameof(ANALISTA);
            public const string ADMINISTRATIVO = nameof(ADMINISTRATIVO);
        }
        public static class ParceiroCargos
        {
            public const string EMPRESA = nameof(EMPRESA);
            public const string ANALISTA = nameof(ANALISTA);
            public const string ADMINISTRATIVO = nameof(ADMINISTRATIVO);
        }
    }
}
