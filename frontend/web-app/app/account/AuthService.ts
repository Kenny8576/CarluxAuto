// /* eslint-disable @typescript-eslint/no-explicit-any */
// import { fetchWrapper } from "@/lib/fetchWrapper";
// import { Identity, LoginDetails, ResetObject } from "@/types";
// import { handleRequest } from "@/utils/AxiosClient";

// type Props = {
//     newUser: Identity
//     loginDetails: LoginDetails
//     email: string
// }

// type ResetObj = {
//   resetObj: ResetObject
// }


// export async function signup({newUser}: Props) {
//     return await handleRequest({
//         loadingMessage: "Signing up...",
//         successMessage: "Signup successful, Please confirm your email!",
//         errorMessage: "Something went wrong, please try again.",
//         request: () => fetchWrapper.post('/Account/register', newUser)
//     });
// }

// export async function loginUser({loginDetails}: Props) {
    
//     const result = await handleRequest({
//         loadingMessage: "Logging in...",
//         successMessage: "Login successful!",
//         errorMessage: "Something went wrong, please try again.",
//         request: () => fetchWrapper.post('/Account/login', loginDetails)
//     });

//     if (result) {
//         return result.token;
//     }
// }

// export async function forgotPassword({email}: Props) {
//     const result = await handleRequest({
//         loadingMessage: "Sending password reset email...",
//         successMessage: "Password reset email sent!",
//         errorMessage: "Failed to send password reset email, please try again.",
//         request: () => fetchWrapper.post(`/Account/forgot-password?email=${email}`)
//     });
// }

// export async function resetPassword({resetObj}: ResetObj){
//     const result = await handleRequest({
//         loadingMessage: "Resetting your password...",
//         successMessage: "Password successfully reset",
//         errorMessage: "Failed to reset password, please try again.",
//         request: () => fetchWrapper.post("/Account/reset-password", resetObj)
//     });
// }
