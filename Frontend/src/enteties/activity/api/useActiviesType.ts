import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { urls } from "../../../shared/api/api.ts";
export type ActivityType = {
    id: number;
    title: string;
};
export const useActiviesType = () => {
    const { activity } = urls;
    const { request, loading } = useHttp();

    const [activityTypes, setActivityTypes] = useState<ActivityType[]>([]);

    const fetchActivityTypes = async () => {
        const res = await request(activity + "types");

        setActivityTypes(res);
    };

    return { activityTypesLoading: loading, activityTypes, fetchActivityTypes };
};
