using LinkwayAPI.Constants.API;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;

        public DashboardRepository(LinkwayDbContext dbContextLinkway)
        {
            _dbContextLinkway = dbContextLinkway;
        }
        public async Task<object> GetAdminCountAsync()
        {
            try
            {
                var totalRequestCount = await _dbContextLinkway.RstRequests.CountAsync();
                var totalPendingRequestCount = await _dbContextLinkway.RstRequests.Where(r => r.RequestStatus == 0).CountAsync();
                var totalApprovedRequestCount = await _dbContextLinkway.RstRequests.Where(r => r.RequestStatus == 1).CountAsync();
                var totalRejectedRequestCount = await _dbContextLinkway.RstRequests.Where(r => r.RequestStatus == 2).CountAsync();

                var totalApprovedGiftApplications = await _dbContextLinkway.CndGiftforms.Where(g => g.GiftformAdminStatus == 1).GroupBy(g => g.ModificationDate.Year.ToString()).Select(group => new { x = group.Key, y = group.Count() }).OrderBy(g => g.x).ToListAsync();
                var totalApprovedNominations = await _dbContextLinkway.NmsNominations.Include(n => n.InternalProgram).Where(n => n.NominationStatus == 1).GroupBy(n => n.InternalProgram.InternalProgramName).Select(group => new { name = group.Key, data = group.GroupBy(g => g.ModificationDate.Year.ToString()).Select(group => new { x = group.Key, y = group.Count() }).OrderBy(g => g.x).ToList() }).ToListAsync();

                var totalGiftApplicationCount = await _dbContextLinkway.CndGiftforms.CountAsync();
                var totalPendingGiftApplicationCount = await _dbContextLinkway.CndGiftforms.Where(g => g.GiftformManagerStatus == 1 && g.GiftformAdminStatus == 0).CountAsync();
                var totalApprovedGiftApplicationCount = await _dbContextLinkway.CndGiftforms.Where(g => g.GiftformAdminStatus == 1).CountAsync();
                var totalRejectedGiftApplicationCount = await _dbContextLinkway.CndGiftforms.Where(g => g.GiftformAdminStatus == 2).CountAsync();

                var totalBusinessUnitsCount = await _dbContextLinkway.MstBusinessUnits.CountAsync();
                var totalInternalProgramCount = await _dbContextLinkway.MstInternalPrograms.CountAsync();
                var totalEmployeesCount = await _dbContextLinkway.Users.CountAsync();

                var totalNominationsCount = await _dbContextLinkway.NmsNominations.CountAsync();
                var totalPendingNominationsCount = await _dbContextLinkway.NmsNominations.Where(g => g.NominationStatus == 0).CountAsync();
                var totalApprovedNominationsCount = await _dbContextLinkway.NmsNominations.Where(g => g.NominationStatus == 1).CountAsync();
                var totalRejectedNominationsCount = await _dbContextLinkway.NmsNominations.Where(g => g.NominationStatus == 2).CountAsync();

                var giftInternalProgram = new { name = AdminConstant.GIFT, data = totalApprovedGiftApplications };

                var gitfPromotionsPercentage = totalApprovedGiftApplications.Select(ga => new { x = ga.x, y = Math.Round((ga.y / (decimal)totalEmployeesCount) * 100, 2) });

                var response = new
                {
                    totalRequestCount,
                    totalPendingRequestCount,
                    totalApprovedRequestCount,
                    totalRejectedRequestCount,
                    totalGiftApplicationCount,
                    totalPendingGiftApplicationCount,
                    totalApprovedGiftApplicationCount,
                    totalRejectedGiftApplicationCount,
                    totalBusinessUnitsCount,
                    totalInternalProgramCount,
                    totalEmployeesCount,
                    totalNominationsCount,
                    totalPendingNominationsCount,
                    totalApprovedNominationsCount,
                    totalRejectedNominationsCount,
                    giftInternalProgram,
                    totalApprovedNominations,
                    gitfPromotionsPercentage
                };

                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
