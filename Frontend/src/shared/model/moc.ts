// Мок модель для PIB
import {
    Activity,
    Group,
    PIB,
    StudentActivities,
    StudentComplete,
    StudentSimple,
    Subject
} from "./types.ts";

export const pibMock: PIB = {
    lastname: "Иванов",
    firstname: "Иван",
    patronymic: "Иванович"
};

// Мок модель для StudentSimple
export const studentSimpleMock: StudentSimple = {
    ...pibMock,
    id: 1,
    averageScore: 85,
    presences: 20,
    absences: 5
};

// Мок модель для Activity
export const activityMock: Activity = {
    subject: "Математика",
    presence: true,
    grade: 20,
    date: "2024-03-02T12:00:00Z"
};
export const activityMock2: Activity = {
    subject: "Фізика",
    presence: true,
    grade: 90,
    date: "2024-03-02T12:00:00Z"
};

// Мок модель для Subject
export const subjectMock: Subject = {
    title: "Математика",
    isExam: false
};

// Мок модель для StudentActivities
export const studentActivitiesMock: StudentActivities = {
    subject: subjectMock,
    activities: [activityMock]
};

// Мок модель для StudentComplete
export const studentCompleteMock: StudentComplete = {
    ...pibMock,
    mobileNumber: "123456789",
    email: "ivanov@example.com",
    subject: "SPR - 321",
    Activities: [activityMock, activityMock2]
};

// Мок модель для Group
export const groupMock: Group = {
    code: "G1",
    subjects: [subjectMock]
};
