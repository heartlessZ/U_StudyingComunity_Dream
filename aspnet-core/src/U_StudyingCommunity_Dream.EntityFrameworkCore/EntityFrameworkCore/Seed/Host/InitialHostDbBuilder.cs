namespace U_StudyingCommunity_Dream.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly U_StudyingCommunity_DreamDbContext _context;

        public InitialHostDbBuilder(U_StudyingCommunity_DreamDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            //new DefaultBookCategoryCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
