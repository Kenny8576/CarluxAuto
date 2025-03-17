import NextAuth, { AuthOptions } from "next-auth";
import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

export const authOptions: AuthOptions = {
  providers: [
    DuendeIdentityServer6({
      id: "id-server",
      clientId: process.env.CLIENT_ID ?? "nextApp",
      clientSecret: process.env.CLIENT_SECRET ?? "secret",
      issuer: process.env.ID_URL, // Ensure this is set in .env
      authorization: { params: { scope: "openid profile auctionApp" } },
    }),
  ],
  session: {
    strategy: "jwt",
  },
  callbacks: {
    async jwt({ token, profile, account}) 
     
    {
      if(profile){
        token.username = profile.username
      }
      if(account){
        token.access_token = account.access_token
      }

      return token
    },
    async session({session, token}) {
      if (token) {
          session.user.username = token.username
      }
      return session;
    }

  },
  secret: process.env.NEXTAUTH_SECRET,
};

export const  handlers= NextAuth(authOptions);
