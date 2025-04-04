import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'


const apiTarget = process.env.services__api__https__0 ||
    process.env.services__api__http__0 ||
    'https://localhost:7548';
export default defineConfig({
    plugins: [
        react(),
        tailwindcss(), // Make sure TailwindCSS works
    ],

    server: {
        port: process.env.PORT || 5468, // Use environment variable for PORT
        proxy: {
            '/api': {
                target: apiTarget, // API target from environment variable or fallback
                rewrite: (path) => path.replace(/^\/api/, ''), // Remove "/api" from the start
                secure: false, // Disable SSL verification (if using localhost with self-signed certs)
                changeOrigin: true, // Ensure the correct host headers are set
            },
        },
        historyApiFallback: true, // Enable for React Router or SPA navigation
    },
})
// devServer: {
//     port: env.PORT || 5468,
//         allowedHosts: "all",
//         historyApiFallback: true,
//         proxy: [{
//         context: ['/api'],
//         target: apiTarget,
//         pathRewrite: { "^/api": "" },
//         secure: false,
//         changeOrigin: true
//     }]
// },