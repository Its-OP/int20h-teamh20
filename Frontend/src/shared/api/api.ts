const baseApiURL = process.env.BASE_URL || "http://localhost:8080/";

export const urls = {
    auctions: `${baseApiURL}auctions/`,
    signInUrl: `${baseApiURL}users/signIn`,
    signUpUrl: `${baseApiURL}users/signUp`
};
