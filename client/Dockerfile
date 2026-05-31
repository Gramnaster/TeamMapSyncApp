# ── TanStack Start / Nitro SSR client ────────────────────────────────────────

FROM node:22-alpine AS deps
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable && corepack prepare pnpm@latest --activate
WORKDIR /app
# Copy lockfile and manifest first for maximum layer-cache reuse
COPY package.json pnpm-lock.yaml pnpm-workspace.yaml ./
RUN pnpm fetch

FROM deps AS build
ENV CI=true
# Allow native post-install scripts (e.g. unrs-resolver) that pnpm 10 blocks by default.
# The store is already populated by pnpm fetch above so no outbound network is needed.
RUN echo "enable-pre-post-scripts=true" >> .npmrc
RUN pnpm install --frozen-lockfile
COPY . .
RUN pnpm run build
# Nitro output lands in /app/.output

FROM node:22-alpine AS final
WORKDIR /app
# Only ship the Nitro output — no node_modules, no source
COPY --from=build /app/.output .output
EXPOSE 3000
ENV NODE_ENV=production
CMD ["node", ".output/server/index.mjs"]
