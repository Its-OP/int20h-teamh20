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
}

export interface StudentActivities {
    subject: Subject;
    activities: Activity[];
}

export interface StudentComplete extends PIB {
    mobileNumber: string;
    email: string;
    Activities: Activity[];
    subject: string;
}

export interface Group {
    code: string;
    subjects: Subject[];
}
