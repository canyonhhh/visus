class ActivitiesController < ApplicationController
  before_action :set_activity, only: %i[ show edit update destroy ]

  def index
    @activities = Activity.all
  end

  def show
  end

  def new
    @activity = Activity.new
  end

  def edit
  end

  def create
    @activity = Activity.new(activity_params)

    if @activity.save
      redirect_to @activity, notice: "Activity was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def update
    if @activity.update(activity_params)
      redirect_to @activity, notice: "Activity was successfully updated.", status: :see_other
    else
      render :edit, status: :unprocessable_entity
    end
  end

  def destroy
    @activity.destroy!

    redirect_to activities_path, notice: "Activity was successfully destroyed.", status: :see_other
  end

  private
  def set_activity
    @activity = Activity.find(params.expect(:id))
  end

  def activity_params
    params.expect(activity: [ :name, :description, :is_active, :business_id ])
  end
end
