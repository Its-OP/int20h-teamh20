import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { StudentSimple } from "../../../shared/model/types.ts";
import { studentSimpleMock } from "../../../shared/model/moc.ts";
import { urls } from "../../../shared/api/api.ts";

export const useStudents = () => {
    const { students: studentsUrl } = urls;

    const { request, loading } = useHttp();
    const [students, setStudents] = useState<StudentSimple[]>([
        studentSimpleMock
    ]);

    const fetchStudents = async (id: number | string) => {
        const res = await request(`${studentsUrl}?groupId=${id}`);

        setStudents(res);
    };
    const clearStudents = () => setStudents([]);

    return {
        studentsLoading: loading,
        students,
        fetchStudents,
        clearStudents
    };
};
