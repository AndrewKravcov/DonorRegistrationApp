import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';
import cssInjectedByJsPlugin from "vite-plugin-css-injected-by-js";

const target = 'https://localhost:7107';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(),cssInjectedByJsPlugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    build: {
      rollupOptions: {
        external: ['vue'],
        output: {
          manualChunks: undefined,
          globals: {
            vue: 'Vue',
          },
        },
      },
    },
    server: {
        proxy: {
            '^/api': {
                target,
                secure: false
            }
        },
        host: '127.0.0.1',
        port: 5173
    }
})
