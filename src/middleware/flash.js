module.exports = (req, res, next) => {
  if (!req.session) {
    return next();
  }

  const setFlash = (type, message) => {
    if (!req.session.flashMessages) {
      req.session.flashMessages = [];
    }
    req.session.flashMessages.push({ type, message });
  };

  req.flash = setFlash;

  const messages = req.session.flashMessages || [];
  res.locals.flashMessages = messages;
  delete req.session.flashMessages;

  next();
};
