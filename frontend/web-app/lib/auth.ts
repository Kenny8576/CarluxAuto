import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"
import Google from "next-auth/providers/google"
 
export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [Google, Credentials({
    credentials:{
      email: { label: "Email", type: "email", placeholder: "your@email.com" },
      password: { label: "Password", type: "password" },
    },
    authorize: async(Credentials) =>{
      const email = "";
      const password = "";

      if(Credentials.email === email && Credentials.password === password)
      {
        return {email, password};
      } else throw Error("Invalid credentials");
    }
  })],
    session: {
    strategy: "jwt",
  },
  callbacks: {
    async jwt({ token, profile, account }) {
        if (profile) {
            token.username = profile.username as string; // Ensure it's a string
        }
        if (account) {
            token.access_token = account.access_token as string;
        }
        return token;
    },
    async session({ session, token }) {
        if (token.username && typeof token.username === "string") {
            session.user.email = token.username; 
        }
        return session;
    }
},
  pages: {
    signIn: "/login",
  },
  secret: process.env.AUTH_SECRET,
})

