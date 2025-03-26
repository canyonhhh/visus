const HTMLWebpackPlugin = require("html-webpack-plugin");

module.exports = (env) => {
  // Get API URL from environment with fallback
  const apiTarget = process.env.services__api__https__0 ||
      process.env.services__api__http__0 ||
      "https://localhost:7548";

  return {
    entry: "./src/index.js",
    devServer: {
      port: env.PORT || 5468,
      allowedHosts: "all",
      historyApiFallback: true,
      proxy: [{
        context: ['/api'],
        target: apiTarget,
        pathRewrite: { "^/api": "" },
        secure: false,
        changeOrigin: true
      }]
    },
    output: {
      path: `${__dirname}/dist`,
      filename: "bundle.js",
    },
    plugins: [
      new HTMLWebpackPlugin({
        template: "./src/index.html",
        favicon: "./src/favicon.ico",
      }),
    ],
    module: {
      rules: [
        {
          test: /\.js$/,
          exclude: /node_modules/,
          use: {
            loader: "babel-loader",
            options: {
              presets: [
                "@babel/preset-env",
                ["@babel/preset-react", { runtime: "automatic" }],
              ],
            },
          },
        },
        {
          test: /\.css$/,
          exclude: /node_modules/,
          use: ["style-loader", "css-loader"],
        },
      ],
    },
  };
};