import { useState } from "react";
import { Subject } from "../../../shared/model/types.ts";
import { useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";
import { message } from "antd";

export const useSubjects = () => {
    const { subject: subjectsUrl } = urls;
    const { request, loading } = useHttp();
    const [subjects, setSubjects] = useState<Subject[]>([]);

    const fetchSubjects = async () => {
        const res = await request(subjectsUrl);

        if (res) {
            setSubjects(res);
        } else {
            message.error("При завантажені предметів щось пішло не так");
        }
    };

    return { fetchSubjects, subjects, subjectsLoading: loading };
};
