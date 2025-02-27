import { handlers } from "@/lib/auth";
export const { GET, POST } = handlers




// import NextAuth, { NextAuthConfig } from "next-auth";
// import CredentialsProvider from "next-auth/providers/credentials";

// export const authConfig: NextAuthConfig = {
//   providers: [
//     CredentialsProvider({
//       id: "credentials",
//       clientSecret: 'secret',
//       issuer: 'http://localhost:5000',
//       authorization: {params: {scope: 'openid profile auctionApp'}},

//       credentials: {
//         email: { label: "Email", type: "email", placeholder: "your@email.com" },
//         password: { label: "Password", type: "password" },
//       },

//       async authorize(credentials) {
//         const res = await fetch("http://localhost:5000/api/auth/login", {
//           method: "POST",
//           headers: { "Content-Type": "application/json" },
//           body: JSON.stringify({
//             email: credentials?.email,
//             password: credentials?.password,
//           }),
//         });

//         const data = await res.json();

//         if (!res.ok) {
//           throw new Error(data.message || "Login failed");
//         }

//         return {
//           id: data.userId,
//           name: data.username,
//           email: credentials?.email,
//           accessToken: data.token,
//           roles: data.roles,
//         };
//       },
//     }),
//   ],
//   session: {
//     strategy: "jwt",
//   },
//   callbacks: {
//     async jwt({ token, profile, account }) {
//         if (profile) {
//             token.username = profile.username as string; // Ensure it's a string
//         }
//         if (account) {
//             token.access_token = account.access_token as string;
//         }
//         return token;
//     },
//     async session({ session, token }) {
//         if (token.username && typeof token.username === "string") {
//             session.user.email = token.username; 
//         }
//         return session;
//     }
// },
//   pages: {
//     signIn: "/login",
//   },
//   secret: process.env.AUTH_SECRET,
// };

// export const { handlers, auth } = NextAuth(authConfig);


// import { Auth } from "@auth/core"
// import Google from "@auth/core/providers/google"
 
// const request = new Request(origin)
// const response = await Auth(request, {
//   providers: [
//     Google({ clientId: GOOGLE_CLIENT_ID, clientSecret: GOOGLE_CLIENT_SECRET }),
//   ],
// })