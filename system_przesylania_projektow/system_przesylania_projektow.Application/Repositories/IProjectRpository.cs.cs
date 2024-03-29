﻿using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Repositories;

public interface IProjectRpository {
    Task CreateProject(Project project);
    Task<Project?> GetProjectById(Guid id);
    Task<IEnumerable<Project>> GetAllProjects();
    Task UpdateProject(Project project);
    Task DeleteProject(Project project);
}
