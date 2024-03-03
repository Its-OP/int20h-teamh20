const baseApiURL = "https://h20-backend.azurewebsites.net/api/";

export const urls = {
    auctions: `${baseApiURL}auctions/`,
    signInUrl: `${baseApiURL}users/signIn`,
    signUpUrl: `${baseApiURL}users/signUp`,

    students: `${baseApiURL}students/`,

    groups: `${baseApiURL}groups`,

    subject: `${baseApiURL}subjects`,

    messages: `${baseApiURL}messages`,
    templatesMessages: `${baseApiURL}messages/templates`
};
