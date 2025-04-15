/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    serverActions: true,
  },
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'cdn.pixabay.com',
        pathname: '/**',
      },
      {
        protocol: 'https',
        hostname: 'res.cloudinary.com',
        pathname: '/**',
      },
      {
        protocol: 'https',
        hostname: 'hips.hearstapps.com',
        pathname: '/**',
      },
    ],
  },
  output: 'standalone',
};

module.exports = nextConfig;



// const nextConfig: NextConfig = {
//   images: {
//     domains: [
//       'cdn.pixabay.com',
//       "hips.hearstapps.com"
//     ],

//     remotePatterns: [
//       {
//         protocol: 'https',
//         hostname: 'res.cloudinary.com',
//         pathname: '/**',
//       }
//     ]
//   },
//   output: 'standalone'
// };

// export default nextConfig;

