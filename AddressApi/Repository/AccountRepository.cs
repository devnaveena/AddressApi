using AddressApi.Contracts;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;

namespace AddressApi.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly RepositoryContext _context;
        public AccountRepository(RepositoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// to get the list of users
        /// </summary>
        /// <returns></returns>
        //get all the users from the database
        public List<User> GetUsers()
        {
            List<User> users = _context.User.ToList();
            return users;
        }
        public RefSet Matches(string type)
        {
            RefSet result = _context.RefSet.FirstOrDefault(o => o.Name.Equals(type));
            if (result == null)
                return null;
            return result;

        }
        public List<Email> GetEmail()
        {
            return _context.Emails.ToList();
        }
        /// <summary>
        /// to add a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Guid</returns>
        // to add the new user
        public Guid Create(User user)
        {
            _context.User.Add(user);

            return user.Id;
        }
        /// <summary>
        /// to save all the changes in the database
        /// </summary>
        //to save
        public void Save()
        {
            _context.SaveChanges();
        }
        /// <summary>
        /// sorting
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<User> GetUsers(Pagination pagination)
        {
            var sort = _context.User as IQueryable<User>;
            if (pagination.SortBy == "FirstName")
                sort = sort.OrderBy(x => x.FirstName);
            if (pagination.SortBy == "LastName")
                sort = sort.OrderBy(x => x.LastName);
            if (pagination.SortOrder == "DSC" && pagination.SortBy == "FirstName")
                sort = sort.OrderByDescending(x => x.FirstName);
            if (pagination.SortOrder == "DSC" && pagination.SortBy == "LastName")
                sort = sort.OrderByDescending(x => x.LastName);
            return sort.Skip(pagination.pageSize * (pagination.pageNumber - 1)).Take(pagination.pageSize).ToList();
        }
        /// <summary>
        /// return address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //for address
        public List<Address> GetAddress(Guid id)
        {
            List<Address> addresses = new List<Address>();
            foreach (Address i in _context.Address)
            {
                if (i.UserId == id)
                {
                    addresses.Add(i);
                }
            }
            return addresses;
        }
        public RefSet Key(Guid id)
        {
            var result = _context.RefSet.FirstOrDefault(o => o.RefSetId.Equals(id));
            if (result == null)
                return null;
            return result;
        }
        /// <summary>
        /// return list of email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //for email
        public List<Email> GetEmail(Guid id)
        {
            Email email = _context.Emails.FirstOrDefault(o => o.UserId == id);
            List<Email> emails = new List<Email>();
            foreach (Email i in _context.Emails)
            {
                if (i.UserId == id)
                {
                    emails.Add(i);
                }
            }
            return emails;
        }
        /// <summary>
        ///  return the phone number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public List<PhoneNumber> GetPhoneNumber(Guid id)
        {
            PhoneNumber phoneNumber =_context.PhoneNumber.FirstOrDefault(o => o.UserId == id);
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
            foreach (PhoneNumber i in _context.PhoneNumber)
            {
                if (i.UserId == id)
                    phoneNumbers.Add(i);
            }
            return phoneNumbers;
        }
        /// <summary>
        /// to find the user by using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // to find
        public Object Findbyid(Guid id)
        {
            User result = _context.User.FirstOrDefault(o => o.Id == id);
            if (result == null)
                return null;
            if (result.IsActive == false)
                return null;
            return result;
        }
        public void Update(User user)
        {
           _context.User.Update(user);
        }
        /// <summary>
        /// to delete by using the user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            User user = _context.User.FirstOrDefault(o => o.Id == id);
            if (user == null) return false;

            _context.User.Remove(user);

           
            if (user.IsActive == false) return false;
            user.IsActive = false;
            return true;
        }
        /// <summary>
        /// for the file upload
        /// </summary>
        /// <param name="image"></param>
        /// <returns>string</returns>
        public void UploadFile(FileModel content)
        {
            _context.File.Add(content);

            

        }
        /// <summary>
        /// download the file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple< FileModel,string> GetFile(Guid id)
        {
            FileModel result = _context.File.FirstOrDefault(o => o.Id.Equals(id));
            if (result == null)
                return null;
            string s = "Downloaded";
            return Tuple.Create(result,s);

        }
        public bool UserExist(Guid userId, UserDto user)
        {
            if (_context.User.Any(a => a.UserName == user.UserName && a.Id == userId))
                return true;

            else if (_context.User.Any(a => a.UserName == user.UserName))
                return (true);
            return false;
        }
        public bool CheckAssetId(Guid assetId, Guid userId)
        {
            return _context.File.Any(a => a.Id == assetId && a.UserId == userId);
        }
        public int CountUsers()
        {
            return (_context.User.Count());

        }
        public Guid Metadata(string key)
        {
            return _context.RefSet.Where(a => a.Name== key).Select(a => a.RefSetId).SingleOrDefault();
        }
    };
}

