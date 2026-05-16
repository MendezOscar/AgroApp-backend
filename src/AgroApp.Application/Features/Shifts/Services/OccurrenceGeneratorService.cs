using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;

namespace AgroApp.Application.Features.Shifts.Services;

public static class OccurrenceGeneratorService
{
    public static List<TaskOccurrence> Generate(
        TaskTemplate template, Guid tenantId)
    {
        var occurrences = new List<TaskOccurrence>();
        var dates = GetDates(template);

        foreach (var date in dates)
        {
            occurrences.Add(new TaskOccurrence
            {
                TemplateId = template.Id,
                TenantId = tenantId,
                ScheduledDate = date,
                Shift = template.Shift,
                Status = Domain.Enums.TaskStatus.Pending,
            });
        }

        return occurrences;
    }

    private static List<DateOnly> GetDates(TaskTemplate template)
    {
        var dates = new List<DateOnly>();
        var start = template.StartDate;
        var end = template.EndDate ?? start;

        switch (template.RecurrenceType)
        {
            case RecurrenceType.Once:
                dates.Add(start);
                break;

            case RecurrenceType.Daily:
                for (var d = start; d <= end; d = d.AddDays(1))
                    dates.Add(d);
                break;

            case RecurrenceType.Weekly:
                var weekDays = ParseWeekDays(template.WeekDays);
                for (var d = start; d <= end; d = d.AddDays(1))
                    if (weekDays.Contains((int)d.DayOfWeek == 0 ? 7 : (int)d.DayOfWeek))
                        dates.Add(d);
                break;

            case RecurrenceType.DateRange:
                for (var d = start; d <= end; d = d.AddDays(1))
                    dates.Add(d);
                break;
        }

        return dates;
    }

    private static List<int> ParseWeekDays(string? weekDays)
    {
        if (string.IsNullOrEmpty(weekDays)) return new List<int>();
        return weekDays.Split(',')
            .Select(d => int.TryParse(d.Trim(), out var n) ? n : -1)
            .Where(n => n > 0)
            .ToList();
    }
}