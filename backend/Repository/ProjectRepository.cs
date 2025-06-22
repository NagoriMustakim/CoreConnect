using AutoMapper;
using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.DTOs.Project;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LinkwayAPI.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        public ProjectRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<ProjectViewDTO> result, int totalCount)> GetAllProjectsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _dbContextLinkway.MstProjects.CountAsync();
                if (pageNumber > 0 && pageSize > 0)
                {
                    var projects = await _dbContextLinkway.MstProjects.OrderByDescending(project => project.CreationDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                    return (_mapper.Map<IEnumerable<ProjectViewDTO>>(projects), totalCount);
                }
                else
                {
                    var projects = await _dbContextLinkway.MstProjects.OrderBy(projects => projects.ProjectTitle).ToListAsync();
                    return (_mapper.Map<IEnumerable<ProjectViewDTO>>(projects), totalCount);
                }
            }
            catch
            {
                return (null, 0);
            }
        }
        public async Task<bool> DoesProjectExists(string projectTitle, Guid? projectGuid)
        {
            try
            {
                var exisitingProject = await _dbContextLinkway.MstProjects.SingleOrDefaultAsync(project => project.ProjectTitle.ToLower() == projectTitle.ToLower());
                if (exisitingProject == null) return false;
                if (exisitingProject.ProjectGuid == projectGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProjectViewDTO> CreateProjectAsync(ProjectAddDTO dtoProjectAdd)
        {
            try
            {
                var mappedValues = _mapper.Map<MstProject>(dtoProjectAdd);
                await _dbContextLinkway.MstProjects.AddAsync(mappedValues);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if(result>0)
                    return (_mapper.Map<ProjectViewDTO>(mappedValues));
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProjectViewDTO> UpdateProjectAsync(ProjectEditDTO dtoProjectEdit)
        {
            try
            {
                var existingProject = await _dbContextLinkway.MstProjects.FirstOrDefaultAsync(project => project.ProjectGuid == dtoProjectEdit.ProjectGuid);
                if (existingProject != null)
                {
                    var updatedProject = _mapper.Map(dtoProjectEdit, existingProject);
                    var result = await _dbContextLinkway.SaveChangesAsync();

                    if (result > 0)
                        return _mapper.Map<ProjectViewDTO>(updatedProject);
                }
                return null;
            }
            catch { return null; }
        }

        public async Task<bool> DeleteProjectAsync(Guid projectGuid)
        {
            try
            {
                var existingProject = await _dbContextLinkway.MstProjects.SingleOrDefaultAsync(project => project.ProjectGuid == projectGuid);
                _dbContextLinkway.MstProjects.Remove(existingProject);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
