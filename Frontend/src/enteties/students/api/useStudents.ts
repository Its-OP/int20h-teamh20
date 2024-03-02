import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { StudentSimple } from "../../../shared/model/types.ts";
import { studentSimpleMock } from "../../../shared/model/moc.ts";

export const useStudents = () => {
    const { request, loading } = useHttp();
    const [students, setStudents] = useState<StudentSimple[]>([
        studentSimpleMock
    ]);

    const fetchStudents = async () => {
        const res = await request(``);

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
