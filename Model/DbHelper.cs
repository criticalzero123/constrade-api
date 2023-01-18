using ConstradeApi.Entity;

namespace ConstradeApi.Model
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns>List of Users</returns>
        public List<UserModel> GetUsers()
        {
            List<UserModel> response = new List<UserModel>();

            var data = _context.Users.ToList();
            data.ForEach(row => response.Add(new UserModel()
            {
                User_id = row.User_id,
                User_status= row.User_status,
                User_type= row.User_type, 
                Authprovider_type= row.Authprovider_type,
                Subscription_type= row.Subscription_type,
                ImageUrl= row.ImageUrl,
                DateCreated= row.DateCreated,
                PersonRefId = row.PersonRef_id,
                Email= row.Email,
                LastActiveAt= row.LastActiveAt,
                CountPost = row.CountPost,
            }));

            return response;
        }
    }
}
