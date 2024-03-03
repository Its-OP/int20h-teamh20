import { userRole } from "../../../features/auth/model/authSlice.ts";

export type NavLink = {
    path: string;
    title: string;
    roles: userRole[];
};
export const links: NavLink[] = [
    {
        path: "/students",
        title: "Студенти",
        roles: [userRole.Professor, userRole.Student]
    },

    {
        path: "/admin",
        title: "Адмінка",
        roles: [userRole.Professor]
    },
    {
        path: "/statistic",
        title: "Статистика",
        roles: [userRole.Professor, userRole.Student]
    }
];
