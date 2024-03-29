const baseApiURL = "http://localhost:5000/api/";

export const urls = {
    signInUrl: `${baseApiURL}users/signIn`,

    students: `${baseApiURL}students`,

    groups: `${baseApiURL}groups`,
    users: `${baseApiURL}users`,

    subject: `${baseApiURL}subjects`,

    messages: `${baseApiURL}messages`,
    templatesMessages: `${baseApiURL}messages/templates`,
    activity: `${baseApiURL}activities/`,
    analyticStudent: `${baseApiURL}analytics/student`
};
