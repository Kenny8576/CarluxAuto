// import { NextApiRequest } from "next";
// import { getToken } from "next-auth/jwt";
// import { cookies, headers } from "next/headers";

// export async function getTokenWorkaround() {
//     const req = {
//         headers: Object.fromEntries(headers() as unknown as Headers),
//         cookies: Object.fromEntries(
//             (await cookies())
//                 .getAll()
//                 .map(c => [c.name, c.value])
//         )
//     } as NextApiRequest;

//     return await getToken({req});
// }