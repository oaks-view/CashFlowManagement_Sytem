module.exports = {
    entry: "./Scripts/App/app.js",
    output: {
        path: "./Scripts/App/dist",
        filename: "app.bundle.js"
    },
    module: {
        loaders: [{
            test: /\.js$/,
            loader: "babel-loader",
            exclude: /node_modules/,
            query: {
                presets: ["es2015", "react"]
            },
        }]
    }
}