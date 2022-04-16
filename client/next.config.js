/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  env: {
    APP_URL: process.env.APP_URL,
    SERVER_URL: process.env.SERVER_URL
  }
}

module.exports = nextConfig;
