using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SM.Core.Common.Enums.Media;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services.Media;
using SM.Core.Interfaces.Services.Membership;
using SM.Infrastructre.Utilities.Extensions;

namespace SM.Infrastructre.Services.Membership
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Follow> _followRepository;
        private readonly IRepository<SearchKeyword> _searchKeywordRepository;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IFileService fileService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _followRepository = _unitOfWork.GetRepository<Follow>();
            _searchKeywordRepository = _unitOfWork.GetRepository<SearchKeyword>();
        }

        public async Task ChangeAvatarImageAsync(string avatarImageName, int userId)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (!string.IsNullOrEmpty(applicationUser.AvatarImagePath))
                await _fileService.DeleteAsync(applicationUser.AvatarImagePath);

            applicationUser.AvatarImagePath = avatarImageName;
            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task<Follow> GetFollowAsync(int followerId, int followeeId)
        {
            return await _followRepository.GetFirstOrDefaultAsync(predicate:follow => follow.FolloweeId == followeeId && follow.FollowerId == followerId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<ApplicationUser> GetUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task UpdatePasswordResetTokenAsync(ApplicationUser applicationUser, string passwordResetToken)
        {
            applicationUser.PasswordResetToken = passwordResetToken;

            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task UpdateRefreshTokenAsync(ApplicationUser applicationUser, string refreshToken)
        {
            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWT:RefreshTokenExpirationTime"]));

            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task InsertFollowAsync(Follow follow)
        {
            await _followRepository.InsertAsync(follow);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void DeleteFollow(Follow follow)
        {
             _followRepository.Delete(follow);
             _unitOfWork.SaveChanges();
        }

        public async Task<IPagedList<Follow>> GetFollowersAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            IQueryable<Follow> followers = _followRepository.GetAll(predicate:follow=>follow.FolloweeId == userId,
                                                                    include:inc=>inc.Include(follow=>follow.Follower));

            return await followers.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public async Task<IPagedList<Follow>> GetFolloweesAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            IQueryable<Follow> followers = _followRepository.GetAll(predicate: follow => follow.FollowerId == userId,
                                                                     include: inc => inc.Include(follow => follow.Followee));

            return await followers.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public async Task<List<SearchKeyword>> GetSearchKeywordsByUserIdAsync(int userId)
        {
            IQueryable<SearchKeyword> searchKeywords = _searchKeywordRepository.GetAll(predicate: searchKeyword => searchKeyword.AuthorId == userId,
                                                                                             orderBy: order => order.OrderByDescending(searchKeywords => searchKeywords.CreatedDate));

            return await searchKeywords.Take(5).ToListAsync();
        }

        public async Task InsertSearchKeywordAsync(SearchKeyword searchKeyword)
        {      
            searchKeyword.CreatedDate = DateTime.Now;
            await _searchKeywordRepository.InsertAsync(searchKeyword);
            await _unitOfWork.SaveChangesAsync();
        }

        public void UpdateSearchKeyword(SearchKeyword searchKeyword)
        {
            searchKeyword.CreatedDate = DateTime.Now;
            _searchKeywordRepository.Update(searchKeyword);
            _unitOfWork.SaveChanges();
        }

        public async Task<SearchKeyword> GetSearchKeywordByKeywordAsync(int userId,string keyword)
        {
            return await _searchKeywordRepository.GetFirstOrDefaultAsync(predicate: searchKeyword => searchKeyword.Keyword.ToLower() == keyword.ToLower());
        }
    }
}
