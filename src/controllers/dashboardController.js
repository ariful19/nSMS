const noticeService = require("../services/noticeService");
const eventService = require("../services/eventService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

exports.index = async (req, res, next) => {
  try {
    const [notices, events] = await Promise.all([
      noticeService.getRecentPublishedNotices(4),
      eventService.getUpcomingEvents(4),
    ]);

    const dashboardState = serializeState({
      notices,
      events,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/dashboard/index", {
      title: "Dashboard",
      notices,
      events,
      scripts: createScriptTag("communications.js"),
      dashboardState,
    });
  } catch (error) {
    next(error);
  }
};
