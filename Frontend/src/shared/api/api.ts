const baseApiURL = "https://h20-backend.azurewebsites.net/api/";

export const urls = {
    signInUrl: `${baseApiURL}users/signIn`,

    students: `${baseApiURL}students`,

    groups: `${baseApiURL}groups`,
    users: `${baseApiURL}users`,

    subject: `${baseApiURL}subjects`,

    messages: `${baseApiURL}messages`,
    templatesMessages: `${baseApiURL}messages/templates`,
    activity: `${baseApiURL}activities/`
};
