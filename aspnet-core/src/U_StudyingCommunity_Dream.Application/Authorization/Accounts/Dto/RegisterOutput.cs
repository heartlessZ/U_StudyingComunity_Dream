namespace U_StudyingCommunity_Dream.Authorization.Accounts.Dto
{
    public class RegisterOutput
    {
        public int Code { get; set; }

        public bool CanLogin { get; set; }

        public string Msg { get; set; }
    }
}
