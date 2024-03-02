namespace backend.ApiContracts;

public record StudentAttendanceContract(string SubjectName, int Presences, int TotalActivities);
public record StudentScoresContract(string SubjectName, int CumulativeScore, int MaxScore);