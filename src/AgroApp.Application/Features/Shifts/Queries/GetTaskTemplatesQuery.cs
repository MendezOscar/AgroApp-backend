using AgroApp.Application.Features.Shifts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Queries;

public record GetTaskTemplatesQuery : IRequest<List<TaskTemplateDto>>;