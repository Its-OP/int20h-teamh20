export type NavLink = {
    path: string;
    title: string;
};
export const links: NavLink[] = [
    {
        path: "/students",
        title: "Студенти"
    },

    {
        path: "/admin",
        title: "Адмінка"
    },
    {
        path: "/statistic",
        title: "Статистика"
    }
];
