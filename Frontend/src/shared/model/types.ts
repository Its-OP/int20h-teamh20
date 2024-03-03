export interface PIB {
    lastname: string;
    firstname: string;
    patronymic: string;
}

export interface StudentSimple extends PIB {
    id: number;
    averageScore: number;
    presences: number;
    absences: number;
}

export interface Activity {
    subject: string;
    presence: boolean;
    grade: number;
    date: string; // ISO8601
}

export interface Subject {
    title: string;
    isExam: boolean;
    id: number;
}

export interface StudentActivities {
    subject: Subject;
    activities: Activity[];
}

export interface StudentComplete extends PIB {
    phoneNumber: string;
    email: string;
    Activities: Activity[];
    subject: string;
    groupCode: string;
}

export interface Group {
    code: string;
    id: number;
    subjects: Subject[];
}
export interface Notification {
    id: number;
    title: string;
    text: string;
    authorId: number;
    sentAt: string;
    isRead: boolean;
}
export interface TemplateNotification {
    id: number;
    title: string;
    text: string;
}
