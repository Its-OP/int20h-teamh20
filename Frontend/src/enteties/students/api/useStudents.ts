import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { StudentSimple } from "../../../shared/model/types.ts";
import { urls } from "../../../shared/api/api.ts";

export const useStudents = () => {
    const { students: studentsUrl } = urls;

    const { request, loading } = useHttp();
    const [students, setStudents] = useState<StudentSimple[]>([]);

    const fetchStudents = async (id: number | string) => {
        const res = await request(`${studentsUrl}group/${id}`);

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
