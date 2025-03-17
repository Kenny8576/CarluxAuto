'use server'

/* eslint-disable @typescript-eslint/no-explicit-any */
import { getServerSession } from "next-auth";
import { authOptions } from "@/auth";
import { getToken } from "next-auth/jwt";
import {cookies, headers} from 'next/headers';
import { NextApiRequest } from "next";


// export async function getSession(): Promise<Session | null> {
//   return await getServerSession(authOptions);
// }

export async function getSession() {
  return await getServerSession(authOptions);
}

export async function getCurrentUser() {
  try {
    const session = await getSession();

    if (!session) return null;

    return session.user;
  
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  } catch (error) {
    return null;
  }
}

export async function getTokenWorkaround() {
  const headersList = await headers(); // ✅ Await headers()
  const cookiesList = await cookies(); // ✅ Await cookies()

  const req = {
      headers: Object.fromEntries(headersList.entries()), // ✅ Correct usage
      cookies: Object.fromEntries(
          cookiesList.getAll().map(c => [c.name, c.value])
      )
  } as NextApiRequest;

  return await getToken({ req });
}


// export async function getTokenWorkaround() {
//   const req = {
//       headers: Object.fromEntries(headers() as unknown as Headers),
//       cookies: Object.fromEntries(
//           (await cookies())
//               .getAll()
//               .map(c => [c.name, c.value])
//       )
//   } as NextApiRequest;

//   return await getToken({req});
// }





// export default async function getTokenWorkaround({req, res}: Props) {
  
//   const token = await getToken({req})

//   if(token) {
//     JSON.stringify(token, null, 2)
//   } else {
//     res.status(401)
//   }

//   res.end()
// } 

// type Props = {
//   req: NextApiRequest
//   res: NextApiResponse
// }

// import type { NextApiRequest, NextApiResponse } from "next";

// export default async function getTokenWorkaround({req, res}: Props) {
//   const token = await getToken({ req, secret: process.env.AUTH_SECRET });

//   if (!token) {
//     return res.status(401).json({ message: "Unauthorized" });
//   }

//   return res.status(200).json({ message: "Success", token });
// }


















