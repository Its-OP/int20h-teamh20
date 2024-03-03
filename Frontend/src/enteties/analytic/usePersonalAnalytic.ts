import { useHttp } from "../../shared/api/useHttp.ts";
import { useState } from "react";
import { urls } from "../../shared/api/api.ts";

export interface personalAnalyticScore {
    subjectName: string;
    cumulativeScore: number;
    maxScore: number;
}
export interface personalAnalyticAttendance {
    subjectName: string;
    presences: number;
    totalActivities: number;
}
export const usePersonalAnalytic = () => {
    const { analyticStudent } = urls;
    const { request, loading } = useHttp();
    const [scoreAnalytic, setScoreAnalytic] = useState<personalAnalyticScore[]>(
        []
    );

    const [attendanceAnalytic, setAttendanceAnalytic] = useState<
        personalAnalyticAttendance[]
    >([]);

    const fetchAnalytic = async (studentId: string) => {
        const attendance = await request(
            `${analyticStudent}/${studentId}/attendance`
        );
        const scores = await request(`${analyticStudent}/${studentId}/scores`);

        setAttendanceAnalytic(attendance);
        setScoreAnalytic(scores);
    };

    return { scoreAnalytic, attendanceAnalytic, loading, fetchAnalytic };
};
