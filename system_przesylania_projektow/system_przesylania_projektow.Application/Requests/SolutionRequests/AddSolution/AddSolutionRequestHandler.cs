﻿using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.AddSolution; 
public class AddSolutionRequestHandler : IRequestHandler<AddSolutionRequest> {
    private readonly IUserContextService _userContext;
    private readonly ISolutionRepository _solutionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITaskRepository _taskRepository;

    public AddSolutionRequestHandler(IUserContextService userConext, ISolutionRepository solutionRepository, IStudentRepository studentRepository, ITaskRepository taskRepository) {
        _userContext = userConext;
        _solutionRepository = solutionRepository;
        _studentRepository = studentRepository;
        _taskRepository = taskRepository;
    }

    public async Task Handle(AddSolutionRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;

        var student = await _studentRepository.GetStudentByUserIdAndProjectId(userId, request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono studenta");

        var task = await _taskRepository.GetTaskById(request.TaskId)
            ?? throw new NotFoundException("Nie znaleziono zadania");


        var memoryStream = new MemoryStream();
        request.File.CopyTo(memoryStream);


        var solution = new ProjectSolution() {
            Id = Guid.NewGuid(),
            FileName = request.File.FileName,
            FileType = request.File.ContentType,
            DocByte = memoryStream.ToArray(),
            StudentId = student.Id,
            TaskId = request.TaskId,
            TaskName = task.Name
        };

        await _solutionRepository.AddSolution(solution);
    }
}
