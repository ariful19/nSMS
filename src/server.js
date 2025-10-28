const http = require("http");
const app = require("./app");
const config = require("./config");

const server = http.createServer(app);

const onListen = () => {
  // eslint-disable-next-line no-console
  console.log(`Server running on http://localhost:${config.port} (${config.env})`);
};

const onError = (error) => {
  if (error.syscall !== "listen") {
    throw error;
  }

  switch (error.code) {
    case "EACCES":
      // eslint-disable-next-line no-console
      console.error(`Port ${config.port} requires elevated privileges`);
      process.exit(1);
      break;
    case "EADDRINUSE":
      // eslint-disable-next-line no-console
      console.error(`Port ${config.port} is already in use`);
      process.exit(1);
      break;
    default:
      throw error;
  }
};

server.on("error", onError);
server.on("listening", onListen);

server.listen(config.port);
