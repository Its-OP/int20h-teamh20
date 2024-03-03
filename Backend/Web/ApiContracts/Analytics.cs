namespace backend.ApiContracts;

public record StudentAttendanceContract(string SubjectName, int Presences, int TotalActivities);
public record StudentScoresContract(string SubjectName, int CumulativeScore, int MaxScore);

public record GroupAttendanceContract(string ConductedAt, int Presences, int TotalActivities);
public record GroupScoresContract(int StudentId, int CumulativeScore, int MaxScore);